using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages.AddressBook
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IRepo<AddressBookEntry> _repo;

        public DeleteModel(IRepo<AddressBookEntry> repo, IMediator mediator)
        {
            _repo = repo;
            _mediator = mediator;
        }

        [BindProperty]
        public AddressBookEntry Entry { get; set; }
        [BindProperty]
        public DeleteAddressRequest DeleteAddressRequest { get; set; }

        public void OnGet(Guid id)
        {
            var _specification = new EntryByIdSpecification(id);
            var _entryList = _repo.Find(_specification);
            if (_entryList.Count > 0)
            {
                Entry = _entryList[0];
                DeleteAddressRequest = new DeleteAddressRequest
                {
                    Id = Entry.Id,
                    Line1 = Entry.Line1
                };
            }
            else
            {
               NotFound();
            }
        }

        public async Task<ActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _ = await _mediator.Send(DeleteAddressRequest);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
