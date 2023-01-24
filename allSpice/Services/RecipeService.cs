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
  internal List<Recipe> GetSearch(string search)
  {
    string newSearch = "%" + search + "%";
    List<Recipe> recipes = _repo.GetSearch(newSearch);
    // List<Recipe> filtered = recipes.FindAll(r=> r.)
    return recipes;
  }

  internal Recipe Get(int id, string userInfo)
  {
    Recipe recipe = _repo.Get(id);
    if (recipe == null)
    {
      throw new Exception("No Recipe at that id");
    }
    return recipe;
  }

  internal Recipe Update(Recipe recipeUpdate, int recipeId, string userId)
  {
    Recipe original = Get(recipeId, userId);
    if (original.creatorId != userId)
    {
      throw new Exception("Wait you didn't make this recipe");
    }
    original.title = recipeUpdate.title ?? original.title;
    original.instructions = recipeUpdate.instructions ?? original.instructions;
    original.img = recipeUpdate.img ?? original.img;
    original.category = recipeUpdate.category ?? original.category;
    bool edited = _repo.Update(original);
    if (edited == false)
    {
      throw new Exception("Recipe was not edited");
    }
    return original;
  }

  internal string Remove(int recipeId, string userId)
  {
    Recipe original = Get(recipeId, userId);
    if (original.creatorId != userId)
    {
      throw new Exception("Nacho recipe but for real tho that is not yours to delete");
    }
    _repo.Remove(recipeId);
    return $"{original.title} has been eaten";
  }
}
