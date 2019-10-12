/**
* DevExtreme (ui/pivot.d.ts)
* Version: 18.2.8
* Build date: Tue Apr 23 2019
*
* Copyright (c) 2012 - 2019 Developer Express Inc. ALL RIGHTS RESERVED
* Read about DevExtreme licensing here: https://js.devexpress.com/Licensing/
*/
import DevExpress from '../bundles/dx.all';

declare global {
interface JQuery {
    dxPivot(): JQuery;
    dxPivot(options: "instance"): DevExpress.ui.dxPivot;
    dxPivot(options: string): any;
    dxPivot(options: string, ...params: any[]): any;
    dxPivot(options: DevExpress.ui.dxPivotOptions): JQuery;
}
}
export default DevExpress.ui.dxPivot;
export type Options = DevExpress.ui.dxPivotOptions;

/** @deprecated use Options instead */
export type IOptions = DevExpress.ui.dxPivotOptions;