using System;
using System.Collections.Generic;
using System.Security.Claims;
using Keeper.Models;
using Keeper.Services;
using Keepr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;
    public KeepsController(KeepsService ks)
    {
      _ks = ks;
    }
    [Authorize]
    [HttpGet("user")]
    public ActionResult<IEnumerable<Keep>> GetKeepsByUser()
    {
      try
      {
        return Ok(_ks.GetKeepsByUser(HttpContext.User.FindFirstValue("Id")));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get()
    {
      try
      {
        return Ok(_ks.Get());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Keep> Get(int id)
    {
      try
      {
        return Ok(_ks.Get(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPost]
    public ActionResult<Keep> Create([FromBody] Keep newKeep)
    {
      try
      {
        newKeep.userId = HttpContext.User.FindFirstValue("Id");
        return Ok(_ks.Create(newKeep));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    public ActionResult<Keep> Edit([FromBody] Keep newKeep, int id)
    {
      try
      {
        newKeep.Id = id;
        return Ok(_ks.Edit(newKeep));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPut("{id}/addKeep")]
    public ActionResult<string> AddKeepToVault([FromBody] Keep keep, int id)
    {
      try
      {
        string userId = HttpContext.User.FindFirstValue("Id");
        return Ok(_ks.AddKeep(id, keep.Id, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [Authorize]
    [HttpDelete("{id}")]
    public ActionResult<string> Delete(int id)
    {
      try
      {
        string userId = HttpContext.User.FindFirstValue("Id");
        return Ok(_ks.Delete(id, userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}