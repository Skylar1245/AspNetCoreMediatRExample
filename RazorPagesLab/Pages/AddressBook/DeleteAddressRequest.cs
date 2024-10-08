using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RazorPagesLab.Pages.AddressBook;

public class DeleteAddressRequest
    : IRequest<Guid>
{
    public Guid Id { get; set; }
    //only included for display purposes
    [DisplayName("Address Line 1")]
	public string Line1 { get; set; }
}