import {CounterComponent} from "./CounterComponent"

// sample class representing the main page
// imports the counter component

export class MainPage {
    
    private counter: CounterComponent
    private mainDiv: HTMLElement

    constructor() {
        this.counter = new CounterComponent()
    }

    render(target: HTMLElement) {
        this.mainDiv = document.createElement("div")
        this.mainDiv.className = "app"
        this.mainDiv.innerHTML =
            "<h1>Tools for Apache Cordova </h1>" +
            "<div id='runtimeInfo' class='info'>initializing..</div>" +
            "<div id='deviceready' class='blink'>" +
            "  <p id='statusP' class='event listening'>Connecting to Device</p>" +
            "</div>"
        target.appendChild(this.mainDiv)
    }

    init() {        
        let statusP: HTMLElement = document.getElementById("statusP")
        statusP.className = "event received"
        statusP.innerText = "Device Ready"
        let divInfo: HTMLElement = document.getElementById("runtimeInfo")
        divInfo.innerText = `running cordova-${window.cordova.platformId }@${window.cordova.version}`

        this.counter.render(this.mainDiv)
    }

}