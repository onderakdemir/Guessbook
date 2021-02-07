using AutoMapper;
using GuessBook.Business.Models;
using GuessBook.Business.Shared;
using GuessBook.EF.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuessBook.EF.Entities;

namespace GuessBook.Business.Managers
{
    public interface IQuestionsService
    {
        Task<ApplicationResult<QuestionsDto>> GetQuestionAsync(string userId, int questionId);
        Task<ApplicationResult<QuestionsDto>> GetQuestionByIdAsync(int questionId);
        Task<ApplicationResult<List<QuestionsDto>>> GetAllQuestionByUserIdAsync(string userId, int qType);
    }
    public class QuestionsService : IQuestionsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public QuestionsService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApplicationResult<List<QuestionsDto>>> GetAllQuestionByUserIdAsync(string userId, int qType)
        {
            try
            {
                if (string.IsNullOrEmpty(userId)) throw new Exception("UserId is null!");

                var userAnsweredQuestionIds = await _context.MemberAnswer.Where(c => c.MemberId == userId).Select(i => i.QuestionId).ToListAsync();

                var query = _context.Questions.Where(c => userAnsweredQuestionIds.Contains(c.Id));
                if (qType != 0)
                {
                    query = query.Where(c => c.QType == qType);

                }
                var questions = await query.ToListAsync();

                return new ApplicationResult<List<QuestionsDto>>
                {
                    ValueResult = _mapper.Map<List<QuestionsDto>>(questions),
                    Succeeded = true
                };
            }
            catch (Exception e)
            {
                return new ApplicationResult<List<QuestionsDto>>
                {
                    Succeeded = false,
                    ErrorMessage = e.Message
                };
            }
        }

        public async Task<ApplicationResult<QuestionsDto>> GetQuestionAsync(string userId, int questionId)
        {
            try
            {
                var userAnsweredQuestionIds = await _context.MemberAnswer.Where(c => c.MemberId == userId).Select(i => i.QuestionId).ToListAsync();

                if (questionId != 0)
                {
                    return await GetQuestionByIdAsync(questionId);
                }

                var total = _context.Questions.Count();
                Questions questions;
                do
                {
                    var offset = new Random().Next(0, total);
               
                    questions = await _context.Questions.Skip(offset).Where(c=>c.Active==true).FirstOrDefaultAsync();
             
                    if (questions != null && userAnsweredQuestionIds.Contains(questions.Id))
                    {
                        questions = null;
                    }
                } while (questions == null);

                return new ApplicationResult<QuestionsDto>
                {
                    ValueResult = _mapper.Map<QuestionsDto>(questions),
                    Succeeded = true
                };

            }
            catch (Exception e)
            {
                return new ApplicationResult<QuestionsDto>
                {
                    Succeeded = false,
                    ErrorMessage = e.Message
                };
            }

        }

        public async Task<ApplicationResult<QuestionsDto>> GetQuestionByIdAsync(int questionId)
        {
            try
            {
                var questions = await _context.Questions.Where(c => c.Id == questionId).FirstOrDefaultAsync();

                return new ApplicationResult<QuestionsDto>
                {
                    ValueResult = _mapper.Map<QuestionsDto>(questions),
                    Succeeded = true
                };

            }
            catch (Exception e)
            {
                return new ApplicationResult<QuestionsDto>
                {
                    Succeeded = false,
                    ErrorMessage = e.Message
                };
            }
        }
    }
}
