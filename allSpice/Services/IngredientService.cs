namespace allSpice.Services;

public class IngredientService
{
  private readonly IngredientRepository _repo;
  private readonly RecipeService _recipeService;
  public IngredientService(IngredientRepository repo, RecipeService recipeService)
  {
    _repo = repo;
    _recipeService = recipeService;
  }
  internal Ingredient Create(Ingredient ingredientData, Account userInfo)
  {
    Recipe recipe = _recipeService.Get(ingredientData.recipeId, userInfo?.Id);

    if (recipe.creatorId != userInfo?.Id)
    {
      throw new Exception("You can't edit another persons recipe");
    }
    Ingredient ingredient = _repo.Create(ingredientData);
    ingredient.recipe = recipe;
    return ingredient;
  }

  internal List<Ingredient> GetRecipeIngredients(int recipeId, string userId)
  {
    Recipe recipe = _recipeService.Get(recipeId, userId);
    List<Ingredient> ingredients = _repo.Get(recipeId);
    if (ingredients == null)
    {
      throw new Exception("No ingredients for that");
    }
    return ingredients;

  }

  internal string Remove(int ingredientId, string userId)
  {
    Ingredient original = _repo.GetOne(ingredientId);
    if (original == null)
    {
      throw new Exception("No ingredient at that id");
    }
    if (original.creatorId != userId)
    {
      throw new Exception("not your recipe");
    }
    _repo.Remove(ingredientId);
    return $"{original.name} has been deleted from the recipe";
  }
}
