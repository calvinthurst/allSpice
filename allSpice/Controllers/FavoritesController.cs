namespace allSpice.Controllers;
[ApiController]
[Route("api/favorites")]

public class FavoritesController : ControllerBase
{
  private readonly Auth0Provider _auth0provider;
  private readonly FavoritesService _favoritesService;

  public FavoritesController(Auth0Provider auth0provider, FavoritesService favoritesService)
  {
    _auth0provider = auth0provider;
    _favoritesService = favoritesService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<RecipeMembers>> Create([FromBody] Favorite favoritesData)
  {
    try
    {
      RecipeMembers userInfo = await _auth0provider.GetUserInfoAsync<RecipeMembers>(HttpContext);
      favoritesData.accountId = userInfo.Id;
      int id = _favoritesService.Create(favoritesData);
      userInfo.RecipeMembersId = id;
      return Ok(favoritesData);

    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<string>> Remove(int id)
  {
    try
    {
      // Console.Out.WriteLine(id);
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _favoritesService.Remove(id, userInfo?.Id);
      return message;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
