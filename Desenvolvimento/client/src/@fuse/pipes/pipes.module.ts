import { NgModule } from '@angular/core';

import { KeysPipe } from './keys.pipe';
import { GetByIdPipe } from './getById.pipe';
import { HtmlToPlaintextPipe } from './htmlToPlaintext.pipe';
import { FilterPipe } from './filter.pipe';
import { CamelCaseToDashPipe } from './camelCaseToDash.pipe';
import { ExcerptPipe } from './excerpt.pipe';
import { RelativeTimePipe } from './relative-time.pipe';
import { MaskPipe } from './mask.pipe';
import { LatinizePipe } from './latinize.pipe';
import { CapitalizePipe } from './capitalize.pipe';

@NgModule({
    declarations: [
        RelativeTimePipe,
        MaskPipe,
        LatinizePipe,
        CapitalizePipe,
        ExcerptPipe,
        KeysPipe,
        GetByIdPipe,
        HtmlToPlaintextPipe,
        FilterPipe,
        CamelCaseToDashPipe
    ],
    imports     : [],
    exports     : [
        RelativeTimePipe,
        MaskPipe,
        LatinizePipe,
        CapitalizePipe,
        ExcerptPipe,
        KeysPipe,
        GetByIdPipe,
        HtmlToPlaintextPipe,
        FilterPipe,
        CamelCaseToDashPipe
    ]
})
export class FusePipesModule
{
}
