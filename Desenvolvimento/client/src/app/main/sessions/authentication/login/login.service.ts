import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SigninViewModel } from '@fuse/types/interfaces/signin-viewmodel';
import { environment } from 'environments/environment';

@Injectable()
export class LoginService {

    private headers: HttpHeaders;

    constructor(private http: HttpClient) {
        this.headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    }

    requestToken(signinData: SigninViewModel): Observable<any> {
        this.headers = this.headers.set('username', signinData.username);
        this.headers = this.headers.set('password', signinData.password);
        const request  = this.http.post(`${environment.apiURL}/account/signin`, {}, {
            headers: this.headers,
            observe: 'response'
        });
        return request;
    }
}
