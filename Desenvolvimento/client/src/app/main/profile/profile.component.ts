import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthenticatedUser } from '@fuse/types/interfaces/authenticated-user.viewmodel';
import { CredentialsService } from '@fuse/core/credentials.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  public user: AuthenticatedUser;
  public roles: string;
  constructor(
    private router: ActivatedRoute,
    private credentialsService: CredentialsService
    ) { }

  ngOnInit() {
    this.user = this.credentialsService.authenticatedUser;
    this.roles = this.user.roles.join(' | ');
  }

}
