using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.SocialMedias.Mediator.Queries.SocialMediaQueries;
using UdemyCarBook.Application.SocialMedias.Mediator.Results;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.SocialMedias.Mediator.Handlers.SocialMediaHandlers.ReadSocialMediaHandlers
{
    public class GetSocialMediaByIdQueryHandler : IRequestHandler<GetSocialMediaByIdQuery, GetSocialMediaByIdQueryResults>
    {
        private readonly IRepository<SocialMedia> _repository;

        public GetSocialMediaByIdQueryHandler(IRepository<SocialMedia> repository)
        {
            _repository = repository;
        }

        public async Task<GetSocialMediaByIdQueryResults> Handle(GetSocialMediaByIdQuery request, CancellationToken cancellationToken)
        {
    
                var values = await _repository.GetByIdAsync(request.Id);
                return new GetSocialMediaByIdQueryResults
                {
                    SocialMediaID = values.SocialMediaID,
                    Name = values.Name,
                    Url = values.Url,
                    Icon = values.Icon,

                };
            
        }
    }
}
