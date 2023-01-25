<template>
  <div class="container">
    <div class="overlay">
      <div class="items"></div>
      <div class="items head">
        <p>{{ recipes.title }}</p>
        <hr>
      </div>
      <div class="items price">
        <p class="old">$699</p>
        <p class="new">$345</p>
      </div>
      <div class="items cart">
        <i class="fa fa-shopping-cart"></i>
        <span>ADD TO CART</span>
      </div>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { Recipe } from "../models/Recipe.js";
import { useRoute } from "vue-router";
export default {
  props: {
    recipes: {
      type: Recipe,
      required: true
    }
  },
  setup(props) {
    const route = useRoute()
    return {
      route,
      coverImg: computed(() => `url(${props.recipes.img})`)
    }
  }
};
</script>


<style lang="scss" scoped>
$bg: #FEF5DF;

body {
  background-color: $bg;
}

.container {

  max-height: 45vh;
  background: v-bind(coverImg);

  background-color: black;
  background-size: cover;
  cursor: pointer;

  -webkit-box-shadow: 0 0 5px #000;
  box-shadow: 0 0 5px #000;
}

.overlay {
  width: inherit;
  height: 45vh;

  display: grid;
  grid-template-columns: 1fr;
  grid-template-rows: 1fr 2fr 2fr 1fr;

  background: rgba(39, 39, 39, 0.9);
  color: $bg;
  opacity: 0;
  transition: all 0.5s;

  font-family: 'Playfair Display', serif;
}


.items {
  padding-left: 20px;
  letter-spacing: 3px;
}

.head {
  font-size: 30px;
  line-height: 40px;

  transform: translateY(40px);
  transition: all 0.7s;

  hr {
    display: block;
    width: 0;

    border: none;
    border-bottom: solid 2px $bg;

    position: absolute;
    bottom: 0;
    left: 20px;

    transition: all .5s;
  }
}

.price {
  font-size: 22px;
  line-height: 10px;
  font-weight: bold;
  opacity: 0;
  transform: translateY(40px);
  transition: all 0.7s;

  .old {
    text-decoration: line-through;
    color: lighten(rgb(77, 77, 77), 40%);
  }
}

.cart {
  font-size: 12px;
  opacity: 0;
  letter-spacing: 1px;
  font-family: 'Lato', sans-serif;
  transform: translateY(40px);
  transition: all 0.7s;

  i {
    font-size: 16px;
  }

  span {
    margin-left: 10px;
  }
}

.container:hover .overlay {
  opacity: 1;

  & .head {
    transform: translateY(0px);
  }

  & hr {
    width: 75px;
    transition-delay: 0.4s;
  }

  & .price {
    transform: translateY(0px);
    transition-delay: 0.3s;
    opacity: 1;
  }

  & .cart {
    transform: translateY(0px);
    transition-delay: 0.6s;
    opacity: 1;
  }
}
</style>