import { CoreModule } from '@abp/ng.core';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { TimeAgoPipe } from './time-ago.pipe';

@NgModule({
  declarations: [],
  imports: [CoreModule, ThemeSharedModule, NgbDropdownModule, NgxValidateCoreModule, TimeAgoPipe],
  exports: [CoreModule, ThemeSharedModule, NgbDropdownModule, NgxValidateCoreModule, TimeAgoPipe],
  providers: [],
})
export class SharedModule {}
