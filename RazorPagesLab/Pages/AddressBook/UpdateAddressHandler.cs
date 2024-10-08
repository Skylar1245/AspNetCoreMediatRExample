using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace RazorPagesLab.Pages.AddressBook;

public class UpdateAddressHandler
    : IRequestHandler<UpdateAddressRequest, Guid>
{
    private readonly IRepo<AddressBookEntry> _repo;

    public UpdateAddressHandler(IRepo<AddressBookEntry> repo)
    {
        _repo = repo;
    }

    public async Task<Guid> Handle(UpdateAddressRequest request, CancellationToken cancellationToken)
    {
        //find the entry based on id
        var _specification = new EntryByIdSpecification(request.Id);
        var entryList = _repo.Find(_specification);

        if (entryList.Count == 0)
            return Guid.Empty; //did not find

        var entry = entryList[0]; // Assuming you want to update the first found entry
        entry.Update(request.Line1, request.Line2, request.City, request.State, request.PostalCode);
        return await Task.FromResult(entry.Id);
    }
}
