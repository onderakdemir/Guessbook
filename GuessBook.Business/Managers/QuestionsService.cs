using AutoMapper;
using GuessBook.Business.Models;
using GuessBook.Business.Shared;
using GuessBook.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessBook.Business.Managers
{
    public interface IQuestionsService
    {
        Task<ApplicationResult<QuestionsDto>> GetQuestionAsync(string userId);
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

                var query =  _context.Questions.Where(c => userAnsweredQuestionIds.Contains(c.Id));
                if (qType != 0)
                {
                    query = query.Where(c => c.QType == qType);

                }
                var questions =await query.ToListAsync();

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

        public async Task<ApplicationResult<QuestionsDto>> GetQuestionAsync(string userId)
        {
            try
            {
                var total = _context.Questions.Count();
                var offset = new Random().Next(0, total);

                var userAnsweredQuestionIds = await _context.MemberAnswer.Where(c => c.MemberId == userId).Select(i => i.QuestionId).ToListAsync();

                var questions = await _context.Questions.Skip(offset)
                    .FirstOrDefaultAsync(x => !userAnsweredQuestionIds.Contains(x.Id));

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
