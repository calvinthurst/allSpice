namespace allSpice.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int Create(Favorite favoritesData)
  {
    string sql = @"
    INSERT INTO favorite
    (accountId, recipeId)
    VALUES
    (@accountId, @recipeId);
    SELECT LAST_INSERT_Id();
    ";
    int id = _db.ExecuteScalar<int>(sql, favoritesData);
    favoritesData.id = id;
    return id;
  }

  internal Favorite
  GetOne(int id)
  {
    string sql = @"
    SELECT
    *
    FROM favorite
    WHERE id = @id;
    ";
    return _db.Query<Favorite>(sql, new { id }).FirstOrDefault();
  }

  internal List<MyRecipes> GetMyRecipes(string id)
  {
    string sql = @"
    SELECT
    re.*,
    fav.*,
    ac.*
    FROM favorite fav
    JOIN recipes re ON re.id = fav.recipeId
    JOIN accounts ac ON re.creatorId = ac.id
    WHERE fav.accountId = @id;
    ";
    List<MyRecipes> myRecipes = _db.Query<MyRecipes, Favorite, Account, MyRecipes>(sql, (re, fav, ac) =>
    {
      re.favoriteId = fav.id;
      re.creator = ac;
      return re;
    }, new { id }).ToList();
    return myRecipes;
  }

  internal void Remove(int id)
  {
    string sql = @"
    DELETE FROM favorite
    WHERE id = @id;
    ";
    _db.Execute(sql, new { id });
  }
}
