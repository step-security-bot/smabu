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

import { Currency } from './currency';
 /**
 * 
 *
 * @export
 * @interface CatalogItemPrice
 */
export interface CatalogItemPrice {

    /**
     * @type {number}
     * @memberof CatalogItemPrice
     */
    price?: number;

    /**
     * @type {Currency}
     * @memberof CatalogItemPrice
     */
    currency?: Currency;

    /**
     * @type {Date}
     * @memberof CatalogItemPrice
     */
    validFrom?: Date;
}
