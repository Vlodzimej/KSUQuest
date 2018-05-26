import { Component, OnInit } from '@angular/core';
import { Group, Question, SafeQuestion } from '../model';
import { ApiService } from '../services';
import { Router } from '@angular/router';

@Component({
    selector: 'app-quest',
    templateUrl: './quest.component.html'
})
export class QuestComponent implements OnInit {

    public groups: Group[];
    public groudId: string;
    public question: SafeQuestion = new SafeQuestion();
    public questions: SafeQuestion[];
    public userAnswer: string;
    public currentQuestionIndex: number = 0;
    public isEnd: boolean = false;

    constructor(private apiService: ApiService, private router: Router) { }

    ngOnInit(): void {
        this.apiService.getAll('groups').subscribe(
            data => {
                this.groups = data as Group[];
                this.groudId = this.groups[0].id;
                this.getQuestions();
            },
            error => {
                console.log(error);
            });
    }

    getQuestions() {
        this.apiService.getById('questions/group', this.groudId).subscribe(
            data => {
                this.questions = data as SafeQuestion[];
                if (this.questions.length > 0) {
                    this.question = this.questions[0];
                }
            }, error => {
                console.log(error);
            });
    }

    checkAnswer() {
        this.apiService.getAll(`questions/answer?questionid=${this.question.id}&value=${this.userAnswer}`).subscribe(
            data => {
                this.currentQuestionIndex++;
                if (this.currentQuestionIndex == this.questions.length) {
                    this.isEnd = true;
                } else {
                    this.question = this.questions[this.currentQuestionIndex];
                }
            },
            error => {
                if (error.status == 404) {
                    console.log('No');
                }
            });
    }
}
