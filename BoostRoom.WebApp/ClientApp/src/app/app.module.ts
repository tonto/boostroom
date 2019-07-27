import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AccountRegistrationModule } from './account-registration/account-registration.module';
import { HttpErrorInterceptor } from './interceptors/http-error.interceptor';
import { ToasterContainerComponent } from './toaster-container/toaster-container.component';
import { ToasterService } from './services/toaster.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    ToasterContainerComponent
  ],
  imports: [
    NgbModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'accounts/register', pathMatch: 'full' }
      // { path: '', component: HomeComponent, pathMatch: 'full' },
      // { path: 'counter', component: CounterComponent },
      // { path: 'fetch-data', component: FetchDataComponent },
    ]),
    AccountRegistrationModule,
  ],
  providers: [
    ToasterService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorInterceptor,
      multi: true,
      deps: [ToasterService]
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
