import {Component, Input, OnInit} from '@angular/core';
import {CdkStepper} from "@angular/cdk/stepper";

@Component({
    selector: 'app-stepper',
    templateUrl: './stepper.component.html',
    styleUrl: './stepper.component.scss',
    providers: [{provide: CdkStepper, useExisting: StepperComponent}]
})
export class StepperComponent extends CdkStepper implements OnInit {
    @Input() linearModeSelected = true;

    onCLick(index: number) {
        this.selectedIndex = index;
    }

    ngOnInit(): void {
        this.linear = this.linearModeSelected;
    }
}
