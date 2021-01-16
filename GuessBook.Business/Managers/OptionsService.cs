using GuessBook.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using GuessBook.EF.Context;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GuessBook.Business.Shared;
using GuessBook.EF.Entities;

namespace GuessBook.Business.Managers
{
    public interface IOptionsService
    {
        Task<ApplicationResult<IEnumerable<OptionsDto>>> GetByQuestionIdAsync(int questionId);
        Task<ApplicationResult> SaveUserAnswers(MemberAnswersDto options);
        Task<ApplicationResult<MemberAnswersDto>> GetUserAnswersAsync(string userId, int questionId);
    }
    public class OptionsService : IOptionsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OptionsService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApplicationResult<IEnumerable<OptionsDto>>> GetByQuestionIdAsync(int questionId)
        {

            try
            {
                var options = await _context.Options.Where(c => c.QuestionId == questionId).ToListAsync();
                return new ApplicationResult<IEnumerable<OptionsDto>>
                {
                    ValueResult = _mapper.Map<IEnumerable<OptionsDto>>(options),
                    Succeeded = true
                };

            }
            catch (Exception e)
            {
                return new ApplicationResult<IEnumerable<OptionsDto>>
                {
                    Succeeded = false,
                    ErrorMessage = e.Message
                };
            }

        }

        public async Task<ApplicationResult<MemberAnswersDto>> GetUserAnswersAsync(string userId, int questionId)
        {
            try
            {
                var options = await _context.MemberAnswer.FirstOrDefaultAsync(c => c.MemberId == userId && c.QuestionId == questionId);

                return new ApplicationResult<MemberAnswersDto>
                {
                    ValueResult = _mapper.Map<MemberAnswersDto>(options),
                    Succeeded = true
                };

            }
            catch (Exception e)
            {
                return new ApplicationResult<MemberAnswersDto>
                {
                    Succeeded = false,
                    ErrorMessage = e.Message
                };
            }
        }

        public async Task<ApplicationResult> SaveUserAnswers(MemberAnswersDto options)
        {
            try
            {
                var userAnswers = _mapper.Map<MemberAnswer>(options);

                var userAnsweredQuestionIds = await _context.MemberAnswer.Where(c => c.MemberId == options.MemberId).Select(i => i.QuestionId).ToListAsync();
                var questionCheck = userAnsweredQuestionIds.Contains(options.QuestionId);
                if (questionCheck) throw new Exception("This question already answered! Skip this question.");

                _context.MemberAnswer.Add(userAnswers);
                var result = await _context.SaveChangesAsync();

                return new ApplicationResult
                {
                    Succeeded = true
                };

            }
            catch (Exception e)
            {
                return new ApplicationResult
                {
                    Succeeded = false,
                    ErrorMessage = e.Message
                };
            }
        }
    }
}
