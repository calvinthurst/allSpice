namespace allSpice.Controllers;
[ApiController]
[Route("api/recipes")]

public class RecipeController : ControllerBase
{
  private readonly RecipeService _recipeService;
  private readonly Auth0Provider _auth0Provider;

  public RecipeController(Auth0Provider auth0Provider, RecipeService recipeService)
  {
    _auth0Provider = auth0Provider;
    _recipeService = recipeService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Recipe>> Create([FromBody] Recipe recipeData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      recipeData.creatorId = userInfo.Id;
      Recipe recipe = _recipeService.Create(recipeData);
      recipe.creator = userInfo;
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet]
  public async Task<ActionResult<List<Recipe>>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Recipe> recipes = _recipeService.Get(userInfo?.Id);
      return Ok(recipes);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Recipe>> Get(int id)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Recipe recipe = _recipeService.Get(id, userInfo?.Id);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{id}")]
  [Authorize]

  public async Task<ActionResult<Recipe>> Update([FromBody] Recipe recipeUpdate, int id)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Recipe recipe = _recipeService.Update(recipeUpdate, id, userInfo?.Id);
      return recipe;
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
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _recipeService.Remove(id, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
