import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {

    public details: any[];
    public games: any[];

    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
        this.updateGames();
    }

    updateGames(): void {        
        this.http.get(this.baseUrl + 'game/index').subscribe(result => {
            this.games = result.json().games;
            this.details = result.json().details;
        }, error => console.error(error));
    }

    createGame(): void {        
        this.http.get(this.baseUrl + 'game/CreateGame').subscribe(result => {            
            this.updateGames();
        }, error => console.error(error));
    }
}
