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
            _response.result = await _specialityService.GetAll();
            _response.isSuccess = true;
            _response.statusCode = HttpStatusCode.OK;

        }catch (Exception ex)
        {
            _response.isSuccess=false;
            _response.statusCode = HttpStatusCode.BadRequest;
            _response.message = ex.Message;
        }
        return Ok(_response);
    }

    [HttpGet("ListadoActivos")]
    public async Task<IActionResult> GetActivos()
    {
        try
        {
            _response.result = await _specialityService.GetActivos();
            _response.isSuccess = true;
            _response.statusCode = HttpStatusCode.OK;

        }
        catch (Exception ex)
        {
            _response.isSuccess = false;
            _response.statusCode = HttpStatusCode.BadRequest;
            _response.message = ex.Message;
        }
        return Ok(_response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SpecialityDto modelDto)
    {
        try
        {
            await _specialityService.Add(modelDto);
            _response.isSuccess = true;
            _response.statusCode = HttpStatusCode.Created;

        }
        catch (Exception ex)
        {
            _response.isSuccess = false;
            _response.statusCode = HttpStatusCode.BadRequest;
            _response.message = ex.Message;
        }
        return Ok(_response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(SpecialityDto modelDto)
    {
        try
        {
            await _specialityService.Update(modelDto);
            _response.isSuccess = true;
            _response.statusCode = HttpStatusCode.NoContent;

        }
        catch (Exception ex)
        {
            _response.isSuccess = false;
            _response.statusCode = HttpStatusCode.BadRequest;
            _response.message = ex.Message;
        }
        return Ok(_response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _specialityService.Remove(id);
            _response.isSuccess = true;
            _response.statusCode = HttpStatusCode.NoContent;

        }
        catch (Exception ex)
        {
            _response.isSuccess = false;
            _response.statusCode = HttpStatusCode.BadRequest;
            _response.message = ex.Message;
        }
        return Ok(_response);
    }

}
