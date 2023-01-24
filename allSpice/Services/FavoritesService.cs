namespace allSpice.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _repo;
  private readonly RecipeService _recipeService;

  public FavoritesService(FavoritesRepository repo, RecipeService recipeService)
  {
    _repo = repo;
    _recipeService = recipeService;
  }

  internal int Create(Favorite favoritesData)
  {
    Recipe recipe = _recipeService.Get(favoritesData.recipeId, favoritesData.accountId);
    int id = _repo.Create(favoritesData);
    return id;

  }

  internal List<MyRecipes> Get(string accountId)
  {
    List<MyRecipes> myRecipes = _repo.GetMyRecipes(accountId);
    return myRecipes;
  }
  internal Favorite Get(int id)
  {
    Favorite favorite = _repo.GetOne(id);
    return favorite;
  }

  internal string Remove(int id, string userInfo)
  {
    Favorite original = _repo.GetOne(id);
    if (original == null)
    {
      throw new Exception("No favorite at that Id");
    }
    if (original.accountId != userInfo)
    {
      throw new Exception("Nacho favorite");
    }
    _repo.Remove(id);
    return $"{original.id} the favorite at that id has been deleted";
  }
}
