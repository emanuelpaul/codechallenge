import { Injectable } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Alert, AlertType } from './alert';

@Injectable({
  providedIn: 'root'
})
export class AlertService {
  private subject = new Subject<Alert>();
  private keepAfterRouteChange = false;

  constructor(private router: Router) {
    // clear alert messages on route change unless 'keepAfterRouteChange' flag is true
    router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        if (this.keepAfterRouteChange) {
          // only keep for a single route change
          this.keepAfterRouteChange = false;
        } else {
          // clear alert messages
          this.clear();
        }
      }
    });
  }

  // subscribe to alerts
  getAlert(alertId?: string): Observable<any> {
    return this.subject.asObservable().pipe(filter((x: Alert) => x && x.alertId === alertId));
  }

  // convenience methods
  success(message: string, keepAfterRouteChange: boolean = false) {
    this.alert(new Alert({ message, type: AlertType.Success, keepAfterRouteChange: keepAfterRouteChange  }));
  }

  error(message: string, keepAfterRouteChange: boolean = false) {
    this.alert(new Alert({ message, type: AlertType.Error, keepAfterRouteChange: keepAfterRouteChange }));
  }

  info(message: string, keepAfterRouteChange: boolean = false) {
    this.alert(new Alert({ message, type: AlertType.Info, keepAfterRouteChange: keepAfterRouteChange  }));
  }

  warn(message: string, keepAfterRouteChange: boolean = false) {
    this.alert(new Alert({ message, type: AlertType.Warning, keepAfterRouteChange: keepAfterRouteChange  }));
  }

  // main alert method    
  alert(alert: Alert) {
    this.keepAfterRouteChange = alert.keepAfterRouteChange;
    this.subject.next(alert);
  }

  // clear alerts
  clear(alertId?: string) {
    this.subject.next(new Alert({ alertId }));
  }
}
