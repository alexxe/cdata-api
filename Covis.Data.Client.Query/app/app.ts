import {MainPage} from "./MainPage"
// main class invoked from index.html
export class App {
    constructor() {
        let mainPage = new MainPage()
        mainPage.render(document.body);
        document.addEventListener("deviceready",() => mainPage.init(), false)
    }
}