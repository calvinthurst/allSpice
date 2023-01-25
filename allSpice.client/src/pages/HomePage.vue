<template>
  <section class="row align-items-center justify-content-center">
    <div v-for="r in recipes" class="col-4 p-2 ">
      <RecipeCard :recipes="r" />
    </div>
  </section>

</template>

<script >
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { recipeService } from "../services/RecipeService.js"
import { ingredientsService } from "../services/IngredientsService.js"
import { onMounted, computed } from "vue";
import { AppState } from "../AppState.js";
import RecipeCard from "../components/RecipeCard.vue";

export default {

  setup() {
    async function getRecipes() {
      try {
        await recipeService.getRecipes()
      } catch (error) {
        logger.log(error)
        Pop.error(error)
      }
    }
    async function getIngredients(recipeId) {
      try {
        await ingredientsService.getIngredients(recipeId)
      } catch (error) {
        logger.log(error)
        Pop.error(error)
      }
    }
    onMounted(() => {
      getRecipes();
    });
    return {
      recipes: computed(() => AppState.recipes)
    };
  }
}
</script>

<style scoped lang="scss">

</style>
