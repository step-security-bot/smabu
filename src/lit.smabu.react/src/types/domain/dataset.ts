/* tslint:disable */
/* eslint-disable */
/**
 * LIT.Smabu.API
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */

import { Serie } from './serie';
 /**
 * 
 *
 * @export
 * @interface Dataset
 */
export interface Dataset {

    /**
     * @type {Array<Serie>}
     * @memberof Dataset
     */
    series?: Array<Serie> | null;

    /**
     * @type {Array<string>}
     * @memberof Dataset
     */
    valueLabels?: Array<string> | null;
}
