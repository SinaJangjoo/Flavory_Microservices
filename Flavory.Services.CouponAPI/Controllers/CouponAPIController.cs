﻿using AutoMapper;
using Flavory.Services.CouponAPI.Data;
using Flavory.Services.CouponAPI.Models;
using Flavory.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Flavory.Services.CouponAPI.Controllers
{
	[Route("api/coupon")]
	[ApiController]
	public class CouponAPIController : ControllerBase
	{
		private readonly AppDbContext _db;
		private ResponseDto _response;
		private IMapper _mapper;

		public CouponAPIController(AppDbContext db, IMapper mapper)
		{
			_db = db;
			_response = new ResponseDto();
			_mapper = mapper;
		}

		[HttpGet]
		public ResponseDto Get()
		{
			try
			{
				IEnumerable<Coupon> objList = _db.Coupons.ToList();
				_response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList); //_mapper.Map<ReturnType>(Source)
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
				Coupon obj = _db.Coupons.First(u => u.CouponId == id);
				_response.Result = _mapper.Map<CouponDto>(obj);  //Convert the obj (Coupon type) to CouponDto
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		[HttpGet]
		[Route("GetByCode/{code}")]
		public ResponseDto GetByCode(string code)
		{
			try
			{
				Coupon obj = _db.Coupons.First(u => u.CouponCode.ToLower() == code.ToLower());
				if (obj == null)
				{
					_response.IsSuccess = false;
				}
				_response.Result = obj;
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		[HttpPost]
		public ResponseDto Post([FromBody] CouponDto couponDto)
		{
			try
			{
				Coupon obj = _mapper.Map<Coupon>(couponDto);
				_db.Coupons.Add(obj);
				_db.SaveChanges();

				_response.Result = _mapper.Map<CouponDto>(obj);  //Map the "obj" back to CouponDto
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		[HttpPut]
		public ResponseDto Put([FromBody] CouponDto couponDto)
		{
			try
			{
				Coupon obj = _mapper.Map<Coupon>(couponDto);
				_db.Coupons.Update(obj);
				_db.SaveChanges();

				_response.Result = _mapper.Map<CouponDto>(obj);  //Map the "obj" back to CouponDto
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		[HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
		{
			try
			{
				Coupon obj = _db.Coupons.First(u=>u.CouponId==id);
				_db.Coupons.Remove(obj);
				_db.SaveChanges();
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}
	}
}
