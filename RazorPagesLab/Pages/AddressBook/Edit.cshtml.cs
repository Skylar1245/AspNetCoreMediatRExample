using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages.AddressBook;

public class EditModel : PageModel
{
    private readonly IMediator _mediator;
    private readonly IRepo<AddressBookEntry> _repo;

    public EditModel(IRepo<AddressBookEntry> repo, IMediator mediator)
    {
        _repo = repo;
        _mediator = mediator;
    }

    [BindProperty]
    public UpdateAddressRequest UpdateAddressRequest { get; set; }

    public void OnGet(Guid id)
    {
        // Todo: Use repo to get address book entry, set UpdateAddressRequest fields.

        //get the address book entry
        var _specification = new EntryByIdSpecification(id);
        var _entryList = _repo.Find(_specification);
        if (_entryList.Count > 0)
        {
            var entry = _entryList[0];
            //load in existing data
            UpdateAddressRequest = new UpdateAddressRequest
            {
                Id = entry.Id,
                Line1 = entry.Line1,
                Line2 = entry.Line2,
                City = entry.City,
                State = entry.State,
                PostalCode = entry.PostalCode
            };
        }
        else
        {
            //error handling for entry not found
            RedirectToPage("Error");
        }
    }

    public async Task<ActionResult> OnPost()
    {
        // Todo: Use mediator to send a "command" to update the address book entry, redirect to entry list.
        if (ModelState.IsValid)
        {
            _ = await _mediator.Send(UpdateAddressRequest);
            return RedirectToPage("Index");
        }
        return Page();
    }
}