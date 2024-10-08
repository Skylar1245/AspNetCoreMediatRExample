using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace RazorPagesLab.Pages.AddressBook;

public class DeleteAddressHandler
	: IRequestHandler<DeleteAddressRequest, Guid>
{
	private readonly IRepo<AddressBookEntry> _repo;

	public DeleteAddressHandler(IRepo<AddressBookEntry> repo)
	{
		_repo = repo;
	}

    public async Task<Guid> Handle(DeleteAddressRequest request, CancellationToken cancellationToken)
    {
        var specification = new EntryByIdSpecification(request.Id);
        var entryList = _repo.Find(specification);

        if (entryList.Count == 0)
            return Guid.Empty; // Entry not found

        var entry = entryList[0];
        _repo.Remove(entry);

        return entry.Id;
    }
}