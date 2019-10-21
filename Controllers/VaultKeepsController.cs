using System;
using System.Collections.Generic;
using System.Security.Claims;
using Keeper.Models;
using Keeper.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;
    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }
    [Authorize]
    [HttpPost]
    public ActionResult<VaultKeep> Create([FromBody] VaultKeep newVK)
    {
      try
      {
        newVK.userId = HttpContext.User.FindFirstValue("Id");
        return Ok(_vks.Create(newVK));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpGet("{id}")]
    public ActionResult<IEnumerable<VaultKeep>> Get(int id)
    {
      try
      {
        return Ok(_vks.Get(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPut]
    public ActionResult<VaultKeep> Delete([FromBody] VaultKeep vaultkeep)
    {
      try
      {
        vaultkeep.userId = HttpContext.User.FindFirstValue("Id");
        return Ok(_vks.Delete(vaultkeep, vaultkeep.userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}