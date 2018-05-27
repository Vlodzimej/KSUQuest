import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-manager',
    templateUrl: './manager.component.html',
})
export class ManagerComponent implements OnInit {
    public isManager: boolean = false;
    public managerPassword: string = "";
    constructor() { }

    ngOnInit(): void { }

    show() {
        console.log('12')
        if (this.managerPassword == "12345") {
            this.isManager = true;
        }
    }
}
