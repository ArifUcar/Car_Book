﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.CQRS.Results.BannerResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.CQRS.Handlers.BannerHandlers.ReadBannerHandlers
{
    public class GetBannerQueryHandler
    {
        private readonly IRepository<Banner> _repository;

        public GetBannerQueryHandler(IRepository<Banner> repository)
        {
            _repository = repository;
        }
        public async Task<List<GetBannerQueryResult>> Handle()
        {
            var values= await _repository.GetAllAsync();
            return values.Select(x=> new GetBannerQueryResult {
            
                BannerId = x.BannerId,
                Description = x.Description,
                VideoDescription = x.VideoDescription,
                VideoURL = x.VideoURL,
                Title = x.Title,

            
            }).ToList();
        }
    }
}
