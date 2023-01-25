export class Ingredient {
  constructor(data) {
    this.id = data.id
    this.name = data.name
    this.quantity = data.quantity
    this.creatorId = data.creatorId
    this.recipeId = data.recipeId
    this.recipe = data.recipe
  }
}