// counter class to show how to manage state
// and basic DOM operations with TypeScript
export class CounterComponent {

    running: boolean = true
    counterDiv: HTMLElement
    buttonControl: HTMLButtonElement

    render(target: HTMLElement) {
        this.counterDiv = document.createElement("div")
        this.counterDiv.className = "counter"
        this.counterDiv.innerText = "0"
        target.appendChild(this.counterDiv)

        this.buttonControl = <HTMLButtonElement>document.createElement("button")
        this.buttonControl.innerText = "Stop"
        this.buttonControl.onclick = () =>  this.buttonClick()
        target.appendChild(this.buttonControl)

        this.updateCounter()
    }

    private buttonClick() {
        this.running = !this.running
        this.buttonControl.innerText = this.running ? "Stop" : "Start"
        this.updateCounter()
    }

    private updateCounter() {
        if (this.running) {
            let n: number = parseInt(this.counterDiv.innerText, 10)
            this.counterDiv.innerText = (n + 1).toString()
            setTimeout(() => {
                this.updateCounter()
            }, 1000)
        }
    }

}