package com.couchbase.workshopapp.web;

import java.util.HashMap;
import java.util.Map;
import java.util.UUID;

import com.couchbase.client.deps.io.netty.util.internal.StringUtil;
import com.couchbase.workshopapp.entity.Person;
import com.couchbase.workshopapp.service.PersonService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import java.util.List;
import com.couchbase.workshopapp.entity.Person;

/**
 * Person controller to handle crud http web requests for person repository
 */
@CrossOrigin()
@RestController
public class PersonController {

    private final PersonService personService;

    @Autowired
    public PersonController(PersonService personService) {
        this.personService = personService;
    }

    /**
     * Find all people in the person repository using select n1ql query
	 *
     * Returns an array of people in the repository
     * Returns a failure status, if the n1ql query failed
     */
    @RequestMapping(value = "/api/getAll", method = RequestMethod.GET)
    public ResponseEntity getAll() {
        Map<String, Object> response = new HashMap<String, Object>();
        try {
            PersonCollection personList = new PersonCollection(personService.getAll());
            return ResponseEntity.ok(personList.persons);
        } catch (Exception ex) {
            response.put("status", "failed");
            response.put("error", ex.getMessage());
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body(response);
        }
    }

    /**
     * Get a single person entity using id stored as couchbase document using
     * a key value lookup on the key
     *
     * @param id Id of the person entity
     * Returns bad request if the id for the person is missing
     * Returns the person entity if get was successful
     * Returns failure status if the get operation was not successful
     */
    @RequestMapping(value = "/api/get/{id}", method = RequestMethod.GET)
    public ResponseEntity get(@PathVariable("id") String id) {
        Map<String, Object> response = new HashMap<String, Object>();
        if (StringUtil.isNullOrEmpty(id)) {
            response.put("status", "failed");
            response.put("error", "missing person id");
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                    .body(response);
        }
        try {
            Person person = personService.get(id);
            if (person != null) {
                return ResponseEntity.ok(person);
            } else {
                throw new Exception("Document not found");
            }
        } catch (Exception ex) {
            response.put("status", "failed");
            response.put("error", ex.getMessage());
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body(response);
        }
    }

    /**
     * Save a person entity
     *
     * @param person json body containing the person's information
     * Returns success if the save operation was successful
     * Returns failure if there was a cas mismatch when using the Version property in person entity
     * or if there are any other errors that caused an operation failure
     */

    @RequestMapping(value = "/api/save", method = RequestMethod.POST)
    public ResponseEntity<Map<String, Object>> save(@RequestBody Person person) {
        Map<String, Object> response = new HashMap<String, Object>();
        if (StringUtil.isNullOrEmpty(person.firstname) || StringUtil.isNullOrEmpty(person.lastname) || StringUtil.isNullOrEmpty(person.email)) {
            response.put("status", "failed");
            response.put("error", "missing person info");
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                    .body(response);
        }
        try {
            if (StringUtil.isNullOrEmpty(person.id)) {
                UUID uuid = UUID.randomUUID();
                person.id = uuid.toString();
            } else {
                // commented out CAS-related code
                //Person existingPerson = personService.get(person.id);
                //person.Version = existingPerson.Version;
            }
            personService.save(person);
            response.put("status", "ok");
            return ResponseEntity.ok(response);
        } catch (Exception ex) {
            response.put("status", "failed");
            response.put("error", ex.getMessage());
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body(response);
        }
    }


    /**
     * Delete a person entity using json containing id property
     *
     * @param req unique identifier for the person {"id":"1"}
     * Return bad request if the id property is missing
     * Returns success if the delete succeeded
     * Return failure if there was a delete operation failure
     */
    @RequestMapping(value = "/api/delete", method = RequestMethod.POST)
    public ResponseEntity<Map<String, Object>> delete(@RequestBody Map<String, String> req) {
        Map<String, Object> response = new HashMap<String, Object>();
        String id = req.get("id");
        if (StringUtil.isNullOrEmpty(id)) {
            response.put("status", "failed");
            response.put("error", "missing id");
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                    .body(response);
        }
        try {
            personService.delete(id);
            response.put("status", "ok");
            return ResponseEntity.ok(response);
        } catch (Exception ex) {
            response.put("status", "failed");
            response.put("error", ex.getMessage());
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body(response);
        }
    }
}