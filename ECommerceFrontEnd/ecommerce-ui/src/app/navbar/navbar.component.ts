import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { logout } from '../auth/store/auth.actions';
import { selectCurrentUser } from '../auth/store/auth.selectors';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  user$: Observable<{ firstName: string; lastName: string } | null>;

  constructor(private store: Store) {
    this.user$ = this.store.select(selectCurrentUser );
  }

  ngOnInit(): void {}

  onLogout() {
    this.store.dispatch(logout());
  }
}