import { Component, OnInit } from '@angular/core';
import { Group, Question } from '../model';
import { ApiService } from '../services';

@Component({
    selector: 'app-editor',
    templateUrl: './editor.component.html'
})
export class EditorComponent implements OnInit {

    public group: Group = new Group();
    public selectedGroupId: string = "";
    public groups: Group[];
    public question: Question = new Question();
    public questions: Question[];

    constructor(private apiService: ApiService) { }

    ngOnInit(): void {
        this.apiService.getAll('groups').subscribe(
            data => {
                this.groups = data as Group[];
                this.selectedGroupId = this.groups[0].id;
                this.getQuestions();
            },
            error => {
                console.log(error);
            });
    }

    getQuestions() {
        this.apiService.getById('questions/group', this.selectedGroupId).subscribe(
            data => {
                this.questions = data as Question[];
            }, error => {
                console.log(error);
            });
    }

    createQuestion() {
        this.question.groupId = this.selectedGroupId;
        this.apiService.create('questions', this.question).subscribe(
            data => {
                this.getQuestions();
            },
            error => {
                console.log(error);
            });
    }

    deleteQuestion() {
        this.apiService.delete('questions', this.question.id).subscribe(
            data => {
                this.getQuestions();
            },
            error => {
                console.log(error);
            }); 
    }

    updateQuestion() {
        this.apiService.update('questions', this.question.id, this.question).subscribe(
            data => {
                this.getQuestions();
            },
            error => {
                console.log(error);
            });
    }

    selectQuestion(question: Question) {
        this.question = {
            id: question.id,
            content: question.content,
            answer: question.answer,
            groupId: question.groupId,
            number: question.number
        }
    }

    createDatabase(){
        this.apiService.create('system/database', {}).subscribe(
            data => {
                console.log(data);
            }, 
            error => {
                console.log(error);
            }
        );
    }
}
