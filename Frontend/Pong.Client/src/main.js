import { createApp } from 'vue'
import App from './App.vue'
import axios from 'axios'
import VueAxios from 'vue-axios'
import VueRouter from 'vue-router'


// const routes = [
//     { path: '/about', template: Pong },
// ]
// const router = VueRouter.createRouter({
//     // 4. Provide the history implementation to use. We are using the hash history for simplicity here.
//     history: VueRouter.createWebHashHistory(),
//     routes, // short for `routes: routes`
// })

const app = createApp(App)
app.use(VueAxios, axios)
// app.use(router)
app.provide('axios', app.config.globalProperties.axios)  // provide 'axios'
app.mount('#app')

