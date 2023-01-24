namespace allSpice.Repositories;

public class IngredientRepository
{
  private readonly IDbConnection _db;

  public IngredientRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Ingredient Create(Ingredient ingredientData)
  {
    string sql = @"
    INSERT INTO ingredients
    (name, quantity, recipeId, creatorId)
    VALUES
    (@name, @quantity, @recipeId, @creatorId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, ingredientData);
    ingredientData.id = id;
    return ingredientData;
  }

  internal List<Ingredient> Get(int recipeId)
  {
    string sql = @"
    SELECT 
    i.*,
    r.*
    FROM ingredients i
    JOIN recipes r ON i.recipeId = r.id
    WHERE recipeId = @recipeId;
    ";
    List<Ingredient> ingredients = _db.Query<Ingredient, Recipe, Ingredient>(sql, (ingredient, recipe) =>
    {
      ingredient.recipe = recipe;
      return ingredient;
    }, new { recipeId }).ToList();
    return ingredients;
  }

  internal Ingredient GetOne(int ingredientId)
  {
    string sql = @"
    SELECT
    *
    FROM ingredients
    WHERE id = @ingredientId
    ";
    return _db.Query<Ingredient>(sql, new { ingredientId }).FirstOrDefault();
  }

  internal void Remove(int ingredientId)
  {
    string sql = @"
    DELETE FROM ingredients
    WHERE id = @ingredientId
    ";
    _db.Execute(sql, new { ingredientId });
  }
}
