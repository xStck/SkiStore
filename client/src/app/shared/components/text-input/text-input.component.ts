import {Component, Input, Self} from '@angular/core';
import {ControlValueAccessor, FormControl, NgControl} from "@angular/forms";

@Component({
    selector: 'app-text-input',
    templateUrl: './text-input.component.html',
    styleUrl: './text-input.component.scss'
})
export class TextInputComponent implements ControlValueAccessor {
    @Input() type = 'text';
    @Input() label = '';


    constructor(@Self() public controlDir: NgControl) {
        this.controlDir.valueAccessor = this;
    }

    get control(): FormControl {
        return this.controlDir.control as FormControl;
    }

    registerOnChange(fn: any): void {
    }

    registerOnTouched(fn: any): void {
    }

    writeValue(obj: any): void {
    }

}
