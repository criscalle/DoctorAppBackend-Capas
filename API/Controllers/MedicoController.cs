using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOS;
using System.Net;

namespace API.Controllers;

[Authorize(Policy = "AdminAgendadorRol")]
public class MedicoController : BaseApiController
{
    private readonly IMedicoService _medicoService;
    private ApiResponse _response;

    public MedicoController(IMedicoService medicoService)
    {
        _medicoService = medicoService;
        _response = new();
    }

    [HttpGet]
    public async Task <IActionResult> Get()
    {
        try
        {
            _response.result = await _medicoService.GetAll();
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

    [HttpPost]
    public async Task<IActionResult> Create(MedicoDto modelDto)
    {
        try
        {
            await _medicoService.Add(modelDto);
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
    public async Task<IActionResult> Update(MedicoDto modelDto)
    {
        try
        {
            await _medicoService.Update(modelDto);
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
            await _medicoService.Remove(id);
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
