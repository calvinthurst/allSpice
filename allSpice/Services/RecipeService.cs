namespace allSpice.Repositories;

public class RecipeService
{
  private readonly RecipeRepository _repo;

  public RecipeService(RecipeRepository repo)
  {
    _repo = repo;
  }

  internal Recipe Create(Recipe recipeData)
  {
    Recipe recipe = _repo.Create(recipeData);
    return recipe;
  }

  internal List<Recipe> Get(string userId)
  {
    List<Recipe> recipes = _repo.Get();
    // List<Recipe> filtered = recipes.FindAll(r=> r.)
    return recipes;
  }

  internal Recipe Get(int id, string userInfo)
  {
    Recipe recipe = _repo.Get(id);
    if (recipe == null)
    {
      throw new Exception("No Recipe at the id");
    }
    return recipe;
  }
}
