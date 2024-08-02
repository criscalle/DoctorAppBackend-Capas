using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTOS;
using System.Net;

namespace API.Controllers;

public class SpecialityController : BaseApiController
{
    private readonly ISpecialityService _specialityService;
    private ApiResponse _response;

    public SpecialityController(ISpecialityService specialityService)
    {
        _specialityService = specialityService;
        _response = new();
    }

    [HttpGet]
    public async Task <IActionResult> Get()
    {
        try
        {
            _response.Result = await _specialityService.GetAll();
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;

        }catch (Exception ex)
        {
            _response.IsSuccess=false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.Message = ex.Message;
        }
        return Ok(_response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SpecialityDto modelDto)
    {
        try
        {
            await _specialityService.Add(modelDto);
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.Created;

        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.Message = ex.Message;
        }
        return Ok(_response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(SpecialityDto modelDto)
    {
        try
        {
            await _specialityService.Update(modelDto);
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.NoContent;

        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.Message = ex.Message;
        }
        return Ok(_response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _specialityService.Remove(id);
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.NoContent;

        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.Message = ex.Message;
        }
        return Ok(_response);
    }

}
