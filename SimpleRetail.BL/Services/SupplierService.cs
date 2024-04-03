using MediatR;
using SimpleRetail.BL.Contracts;
using SimpleRetail.BL.Validations.Enums;
using SimpleRetail.BL.Validations.Supplier;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;
using SimpleRetail.Data.Contracts;

namespace SimpleRetail.BL.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMediator _mediator;

    public SupplierService(ISupplierRepository supplierRepository, IMediator mediator)
    {
        _supplierRepository = supplierRepository;
        _mediator = mediator;
    }

    public async Task<IEnumerable<SupplierDto>> GetAll(int page, int recordsToTake)
    {
        return await _supplierRepository.GetAll(page, recordsToTake);
    }

    public async Task<SupplierDto?> GetById(Guid id)
    {
        return await _supplierRepository.GetById(id);
    }

    public async Task<SupplierDto?> GetDetailsById(Guid id)
    {
        return await _supplierRepository.GetDetailsById(id);
    }

    public async Task<SupplierDto?> CreateAsync(ChangeSupplierRequest request)
    {
        request.Id = Guid.NewGuid();
        request.Active = true;

        var command = new SupplierCommand(request, ChangeAction.INSERT);
        var result = await _mediator.Send(command);
        return result;
    }

    public async Task<SupplierDto?> UpdateAsync(ChangeSupplierRequest request)
    {
        var command = new SupplierCommand(request, ChangeAction.UPDATE);
        var result = await _mediator.Send(command);
        return result;
    }

    public async Task DeleteAsync(Guid id, Guid ChangeUserId)
    {
        await _supplierRepository.DeleteAsync(id, ChangeUserId);
        return;
    }


}
