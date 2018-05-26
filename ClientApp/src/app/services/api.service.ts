import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import { Group } from '../model';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class ApiService {

    constructor(private http:HttpClient) {}
 
    // Uses http.get() to load data from a single API endpoint
    getAll(controller: string) {
        return this.http.get(`/api/${controller}`);
    }

    getById(controller: string, id: string) {
        return this.http.get(`/api/${controller}/${id}`);
    }

    create(controller: string, object: any) {
        return this.http.post(`/api/${controller}`, object);
    }

    delete(controller: string, id: string) {
        return this.http.delete(`/api/${controller}/${id}`);
    }

    update(controller: string, id: string, object: any) {
        return this.http.put(`/api/${controller}/${id}`, object);
    }

}

