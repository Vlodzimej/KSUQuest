import { Component, OnInit } from '@angular/core';
import { Group } from '../model';
import { ApiService } from '../services';

@Component({
    selector: 'app-group',
    templateUrl: './group.component.html',
})
export class GroupComponent implements OnInit {

    public group: Group;
    public groups: Group[];

    constructor(private apiService: ApiService) { }

    ngOnInit(): void {
        this.group = new Group();
        this.getGroups();
    }

    getGroups() {
        this.apiService.getAll('groups').subscribe(
            data => {
                this.groups = data as Group[];
            },
            error => {
                console.log(error);
            });
    }

    createGroup() {
        if (!this.checkGroupName()) {
            this.apiService.create('groups', this.group).subscribe(
                data => {
                    this.group = data as Group;
                    this.getGroups();
                },
                error => {
                    console.log(error);
                });
        }
    }

    selectGroup(group: Group) {
        this.group = {
            id: group.id,
            name: group.name
        };
    }

    deleteGroup() {
        this.apiService.delete('groups', this.group.id).subscribe(
            data => {
                this.getGroups();
            },
            error => {
                console.log(error);
            }
        );
    }

    checkGroupName() {
        if (typeof this.groups !== 'undefined') {
            var group = this.groups.find(g => g.name == this.group.name);
            if (typeof group === 'undefined') {
                console.log(`Group isn't exist`);
                return false;
            }
            console.log(`Group is finded`);
            return true;
        } else {
            return false;
        }
    }
}
