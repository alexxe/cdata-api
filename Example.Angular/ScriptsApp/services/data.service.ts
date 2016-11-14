import {Injectable} from '@angular/core';
import {Http, Response, Headers, RequestOptions} from '@angular/http';
import { Observable } from 'rxjs/observable';

// Statics
import 'rxjs/add/observable/throw';

// Operators
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class ProjectService {
    constructor(private _http: Http) { }

    getProjectMetadata(): Observable<any> {
        let response = this._http.get(`http://localhost/Example.WebApi/api/Model`);
        return response.map((res: Response) => res.json());
    }

    getProjects(descriptor: CQueryDescriptor.CQueryDescriptor): Observable<any> {
        let body = JSON.stringify(descriptor);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this._http.post(`http://localhost/Example.WebApi/api/Model/Default`, body, options)
            .map((res: Response) => res.json()).catch(this.handleError);
    }

    private handleError(error: any) {
        // In a real world app, we might use a remote logging infrastructure
        // We'd also dig deeper into the error to get a better message
        let errMsg = (error.message) ? error.message :
            error.status ? `${error.status} - ${error.statusText}` : 'Server error';
        console.error(errMsg); // log to console instead
        return Observable.throw(errMsg);
    }
}