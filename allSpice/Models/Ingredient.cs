namespace allSpice.Models;

public class Ingredient
{
  public int id { get; set; }
  public string name { get; set; }
  public string quantity { get; set; }
  public string creatorId { get; set; }
  public int recipeId { get; set; }
  public Recipe recipe { get; set; }

}
