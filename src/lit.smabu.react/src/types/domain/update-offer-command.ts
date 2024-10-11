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

import { OfferId } from './offer-id';
import { TaxRate } from './tax-rate';
 /**
 * 
 *
 * @export
 * @interface UpdateOfferCommand
 */
export interface UpdateOfferCommand {

    /**
     * @type {OfferId}
     * @memberof UpdateOfferCommand
     */
    id: OfferId;

    /**
     * @type {TaxRate}
     * @memberof UpdateOfferCommand
     */
    taxRate: TaxRate;

    /**
     * @type {string}
     * @memberof UpdateOfferCommand
     */
    offerDate?: string;

    /**
     * @type {string}
     * @memberof UpdateOfferCommand
     */
    expiresOn?: string;
}
