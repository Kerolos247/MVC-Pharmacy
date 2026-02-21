using WebApplication4.Application.IServices;
using WebApplication4.Domain.Models;
using WebApplication4.Application.Dto.Patient;
using WebApplication4.Application.Common.Results;

namespace WebApplication4.Application.Services
{
    public class FeedBackService : IFeedBackService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeedBackService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> AddAsync(PatientFeedbackDto feedBack)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var feedBackEntity = new PatientFeedback
                {
                    PatientName = feedBack.PatientName,
                    Address = feedBack.Address,
                    PhoneNumber = feedBack.PhoneNumber,
                    Notes = feedBack.Notes,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.feedBack.AddAsync(feedBackEntity);
                await _unitOfWork.CommitAsync();

                return Result<bool>.Success(true);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return Result<bool>.Failure("An error occurred while adding feedback.");
            }
        }

        public async Task<Result<List<PatientFeedbackDto>>> GetAllAsync()
        {
            try
            {
                var data = await _unitOfWork.feedBack.GetAllAsync();

                var result = data.Select(f => new PatientFeedbackDto
                {
                    PatientName = f.PatientName,
                    Address = f.Address,
                    PhoneNumber = f.PhoneNumber,
                    Notes = f.Notes
                }).ToList();

                return Result<List<PatientFeedbackDto>>.Success(result);
            }
            catch (Exception)
            {
                return Result<List<PatientFeedbackDto>>.Failure("An error occurred while retrieving feedback list.");
            }
        }

        public async Task<Result<PatientFeedbackDto?>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.feedBack.GetByIdAsync(id);

                if (entity == null)
                    return Result<PatientFeedbackDto?>.Failure("Feedback not found.");

                var dto = new PatientFeedbackDto
                {
                    PatientName = entity.PatientName,
                    Address = entity.Address,
                    PhoneNumber = entity.PhoneNumber,
                    Notes = entity.Notes
                };

                return Result<PatientFeedbackDto?>.Success(dto);
            }
            catch (Exception)
            {
                return Result<PatientFeedbackDto?>.Failure("An error occurred while retrieving feedback.");
            }
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var entity = await _unitOfWork.feedBack.GetByIdAsync(id);

                if (entity == null)
                {
                    await _unitOfWork.RollbackAsync();
                    return Result<bool>.Failure("Feedback not found.");
                }

                await _unitOfWork.feedBack.DeleteAsync(id);
                await _unitOfWork.CommitAsync();

                return Result<bool>.Success(true);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return Result<bool>.Failure("An error occurred while deleting feedback.");
            }
        }
    }
}