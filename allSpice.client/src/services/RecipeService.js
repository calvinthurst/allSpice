import { AppState } from "../AppState.js"
import { Recipe } from "../models/Recipe.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"
class RecipeService {

  async getRecipes() {
    const res = await api.get('api/recipes')
    AppState.recipes = res.data.map(r => new Recipe(r))
    logger.log(res.data)
  }
}

export const recipeService = new RecipeService()