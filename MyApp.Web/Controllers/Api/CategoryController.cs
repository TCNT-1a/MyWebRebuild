using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Web.Abstraction;
using MyApp.Web.Infra.Data;
using MyApp.Web.Infra.Models.User;

namespace MyApp.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseDtoController<Category, Category, Category>
    {
        private readonly BloggingContext _context;
        private readonly IMapper _mapper;
        public CategoryController(BloggingContext context, ILogger<User> logger, IMapper mapper) : base(context, logger, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
    }
}
