using Flavory.Services.ProductAPI.Models.Dto;
using Flavory.Services.ProductAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flavory.Services.ProductAPI.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Flavory.Services.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ResponseDto _response;
        private readonly AppDbContext _db;
        private IMapper _mapper;
        public ProductController(AppDbContext db, IMapper mapper)
        {
            _response = new ResponseDto();
            _mapper = mapper;
            _db = db;
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Product> products = _db.Products.ToList();
                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(products);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Product obj = _db.Products.First(u => u.ProductId == id);
                _response.Result=_mapper.Map<ProductDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess=false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public ResponseDto GetByName(string name)
        {
            try
            {
            var obj = _db.Products.First(u => u.Name.ToLower() == name.ToLower());
            if (obj==null)
            {
                _response.IsSuccess = false;
            }
            _response.Result=obj;
            }
            catch (Exception ex)
            {
                _response.IsSuccess=false;
                _response.Message=ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles ="ADMIN")]
        public ResponseDto Post([FromBody]ProductDto productDto)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDto);
                _db.Products.Add(product);
                _db.SaveChanges();
                _response.Result=_mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _response.IsSuccess=!false;
                _response.Message=ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody]ProductDto productDto)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDto);
                _db.Products.Update(product);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess=false;
                _response.Message=ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Authorize(Roles = "ADMIN")]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
            Product obj=_db.Products.First(u=>u.ProductId==id);
            _db.Products.Remove(obj);
            _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess=false;
                _response.Message=ex.Message;
            }
            return _response;
        }
    }
}
