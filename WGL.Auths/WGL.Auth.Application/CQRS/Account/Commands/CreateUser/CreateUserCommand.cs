using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGL.Auth.Application.Exceptions;
using WGL.Auth.Application.Interfaces.Account;
using WGL.Auth.Application.Wrappers;
using WGL.Auth.Domain.Entities;

namespace WGL.Auth.Application.CQRS.Account.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Response<int>>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string CompanyName { get; set; }
        public required string MobileNumber { get; set; }
        public required string EmailAddress { get; set; }
        public required string Password { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<int>>
    {
        private readonly IAccountRepositoryAsync _accountRepositoryAsync;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(IAccountRepositoryAsync accountRepositoryAsync,IMapper mapper)
        {
            _accountRepositoryAsync = accountRepositoryAsync;       
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //if (await _accountRepositoryAsync.IsDuplicateUser(request.EmailAddress))
            //    throw new ApiException($"User (" + request.EmailAddress + ") already exist");

            var UserDetials = _mapper.Map<ApplicationUser>(new ApplicationUser() {
                CompanyName = request.CompanyName,
                MobileNumber = request.MobileNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                EmailID=request.EmailAddress,
                CreatedBy=0
            });

           var result= await _accountRepositoryAsync.Sp_CreateUserAsync(UserDetials);
            return new Response<int>(result,message: $"New User Created");

        }
        
    }
    
}
