import { PreviewResumeEvent } from '../previewresumeevent/preview-resume-event';
import { PreviewResumeEventComponent } from '../previewresumeevent/preview-resume-event.component';
import { DateRange } from '../../../../services/resumeservice/date-range';
import { AddResumeEventCommand } from '../../../../services/resumeservice/add-resume-event-command';
import { ResumeEventsService } from '../../../../services/resumeservice/resume-events.service';
import { Component, OnInit, ViewChild, OnDestroy } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
    templateUrl: "./add-resume-event.component.html",
    styleUrls: ["./add-resume-event.component.css"]
})
export class AddResumeEventComponent implements OnInit, OnDestroy {
    _form: FormGroup;
    
    @ViewChild(PreviewResumeEventComponent)
    _previewResumeEvent: PreviewResumeEventComponent;
    _previewObserver: Subscription

    constructor(private _fb: FormBuilder, private _resumeService: ResumeEventsService, private _router: Router) {

    }

    ngOnInit() {
        this._form = this._fb.group({
            startDate: ["", [Validators.required]],
            endDate: ["", [Validators.required]],
            name: ["", [Validators.required, Validators.maxLength(50)]],
            message: ["", [Validators.required, Validators.maxLength(1000)]]
        });

        this._previewObserver = this._form.valueChanges.subscribe(v => {          
            let previewResumeEvent = new PreviewResumeEvent();
            previewResumeEvent.startDate = this._form.get("startDate").value;
            previewResumeEvent.endDate = this._form.get("endDate").value;
            previewResumeEvent.name = this._form.get("name").value;
            previewResumeEvent.message = this._form.get("message").value;

            this._previewResumeEvent.previewResumeEvent = previewResumeEvent;
        })
    }

    ngOnDestroy(): void {
        if (this._previewObserver)
            this._previewObserver.unsubscribe();
    }

    growTextarea(element: HTMLTextAreaElement) {
        element.style.height = `${element.scrollHeight+2}px`;
    }

    onSubmit() {
        if (!this._form.valid) {
            for (let controlName in this._form.controls) {
                this._form.get(controlName).markAsTouched();
            }
            return;
        }

        var addResumeEventCommand = new AddResumeEventCommand();
        var dateRange = new DateRange();
        
        dateRange.startDate = this._form.get("startDate").value;
        dateRange.endDate = this._form.get("endDate").value;
        addResumeEventCommand.dateRange = dateRange;
        addResumeEventCommand.name = this._form.get("name").value;
        addResumeEventCommand.message = this._form.get("message").value;

        this._resumeService.add(addResumeEventCommand).subscribe(r => this._router.navigateByUrl("admin/resume"));
    }
}